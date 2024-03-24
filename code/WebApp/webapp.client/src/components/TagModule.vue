<template>
    <div>
        <p></p>
        <ul class="existing-tags">
            <li v-for="(tag, index) in tags" :key="index" class="tag">
                {{ tag }}
                <button @click="deleteExistingTag(tag)">-</button>
            </li>
            <li v-if="addingNewTag" class="tag">
                <input type="text" v-model="newTagInput" placeholder="New Tag">
                <button @click="cancelAddTag">Cancel</button>
                <button @click="addNewTag">Add</button>
            </li>
        </ul>
        <button @click="toggleAddingNewTag">+</button>
    </div>
</template>

<script>
    export default {
        props: {
            noteId: {
                type: Number,
                required: true
            },
        },
        data() {
            return {
                newTagInput: '',
                tags: [],
                addingNewTag: false
            }
        },
        async mounted() {
            await this.getExistingTags();
        },
        methods: {
            async getExistingTags() {
                try {
                    const response = await fetch(`/Tag/GetTagByNotesID/${this.noteId}`);
                    if (response.ok) {
                        this.tags = await response.json();
                    } else {
                        console.error('Failed to get existing tags');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
            toggleAddingNewTag() {
                this.addingNewTag = !this.addingNewTag;
                this.newTagInput = '';
            },
            cancelAddTag() {
                this.addingNewTag = false;
                this.newTagInput = '';
            },
            async addNewTag() {
                if (this.newTagInput.trim() !== '') {
                    try {
                        const response = await fetch(`/Tag/AddTag`, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify({
                                notesId: this.noteId,
                                tagName: this.newTagInput
                            })
                        });

                        if (response.ok) {
                            this.getExistingTags();
                            this.newTagInput = ''; 
                            this.addingNewTag = false;
                        } else {
                            console.error('Failed to add tag');
                        }
                    } catch (error) {
                        console.error('Error', error);
                    }
                }
            },
            async deleteExistingTag(tag) {
                try {
                    const response = await fetch(`/Tag/RemoveTag`, {
                        method: 'DELETE',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                            notesId: this.noteId,
                            tagName: tag
                        })
                    });

                    if (response.ok) {
                        this.getExistingTags();
                    } else {
                        console.error('Failed to delete tag');
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
