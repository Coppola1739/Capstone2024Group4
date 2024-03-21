<template>
    <div>
        <p></p>
        <ul class="existing-tags">
            <li v-for="(tag, index) in tags" :key="index" class="tag">
                {{ tag }}
                <button @click="addNewTag">+</button>
                <button @click="deleteExistingTag(tag)">-</button>
            </li>
        </ul>
    </div>
</template>


<script>
    export default {
        components: {
        },
        directives: {
        },
        filters: {
        },
        props: {
            noteId: {
                type: Number,
                required: true
            },
        },
        data() {
            return {
                newTag: '',
                tags: []
            }
        },
        async mounted() {
            this.tags = await this.getExistingTags();
        },
        methods: {
            async getExistingTags() {
                try {
                    const response = await fetch(`/Tag/GetTagByNotesID/${this.noteId}`);
                    if (response.ok) {
                        const data = await response.json();
                        return data;
                    } else {
                        console.error('Failed to get existing tags');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
        },
    };
</script>

<style>
</style>