<template>
    <div class="source-module">
        <router-link :to="{ name: 'SourcePage', params: { id: sourceId }, query: { userId: userId } }">
            <div class="source-info">
                <h3>{{ sourceName }}</h3>
                <p>Uploaded on: {{ uploadDate }}</p>
            </div>
        </router-link>
        <span class="delete-icon" @click="deleteSource">&#10006;</span>
    </div>
</template>

<script>
    export default {
        props: {
            sourceId: Number,
            sourceName: String,
            uploadDate: String,
            userId: [Number, String]
        },
        methods: {
            deleteSource() {
                if (confirm("Are you sure you want to delete this source?")) {
                    fetch(`File/deleteSource/${this.sourceId}`, {
                        method: 'DELETE'
                    })
                        .then(response => {
                            if (response.ok) {
                                this.$emit('source-deleted', this.sourceId);
                                alert("Source deleted successfully.");
                            } else {
                                alert("Failed to delete source.");
                            }
                        })
                        .catch(error => {
                            console.error('Error deleting source:', error);
                            alert('Error deleting source: ' + error.message);
                        });
                }
            },
        },
    };
</script>


<style scoped>
    .source-module {
        position: relative;
        border: 1px solid #ccc;
        border-radius: 4px;
        padding: 10px;
        cursor: pointer;
        margin-bottom: 10px;
    }

    .delete-icon {
        position: absolute;
        top: 5px;
        right: 5px;
        cursor: pointer;
        color: red;
    }
</style>
