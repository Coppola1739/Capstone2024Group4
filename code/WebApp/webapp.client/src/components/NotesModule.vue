<template>
    <div class="note">
        <div v-if="!showEdit">
            <div class="note-content">
                {{ isTruncated ? truncatedContent + '...' : note.content }}
            </div>
            <button @click="toggleEdit">View</button>
            <button @click="confirmDelete">Delete</button>
        </div>
        <div v-else>
            <textarea v-model="updatedContent"></textarea>
            <button @click="saveNote">Done</button>
        </div>
        <div class="tags">
            <TagModuleVue :noteId="note.notesId" />
        </div>
    </div>
</template>

<script>
    import TagModuleVue from './TagModule.vue';
    export default {
        components: {
            TagModuleVue
        },
        props: {
            note: {
                type: Object,
                required: true,
            },
        },
        data() {
            return {
                showEdit: false,
                updatedContent: '',
                maxTruncatedLength: 10,
            };
        },
        computed: {
            truncatedContent() {
                return this.note.content.slice(0, this.maxTruncatedLength);
            },
            isTruncated() {
                return this.note.content.length > this.maxTruncatedLength;
            }
        },
        methods: {
            toggleEdit() {
                this.showEdit = !this.showEdit;
                if (this.showEdit) {
                    this.updatedContent = this.note.content;
                }
            },
            isUpdatedContentEmpty() {
                return this.updatedContent.trim() === '';
            },
            async saveNote() {
                try {
                    if (this.isUpdatedContentEmpty()) {
                        alert("Note cannot be empty!");
                        return;
                    }
                    const response = await fetch(`/Notes/UpdateNote/${this.note.notesId}`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(this.updatedContent),
                    });

                    if (response.ok) {
                        console.log('Note updated successfully');
                        this.note.content = this.updatedContent;
                        this.showEdit = false;
                        this.$emit('note-updated');
                    } else {
                        console.error('Failed to update note');
                        this.showEdit = false;
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
            async deleteNote() {
                try {
                    const response = await fetch(`/Notes/DeleteNote/${this.note.notesId}`, {
                        method: 'DELETE',
                    });

                    if (response.ok) {
                        this.$emit('note-updated');
                        alert('Note deleted!');
                    } else {
                        console.error('Failed to delete note');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
            confirmDelete() {
                if (confirm('Are you sure you want to delete this note?')) {
                    this.deleteNote();
                }
            },
        }
    };
</script>

<style scoped>
    .note-content {
        max-height: 2%;
        overflow: initial;
        text-overflow: ellipsis;
        white-space: nowrap;
    }

    .note button {
        background-color: #007bff;
        color: white;
        border-radius: 5%;
        border: 1px solid #007bff;
        cursor: pointer;
    }
        .note button:hover {
            background-color: #0056b3;
        }
</style>
