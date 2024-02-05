<template>
    <div class="note">
        <p>{{ note.content }}</p>
        <button @click="editNoteContent">Edit Note</button>
        <div v-if="showEdit">
            <textarea v-model="updatedContent"></textarea>
            <button @click="saveNote">Save</button>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            note: {
                type: Object,
                required: true,
            },
            noteId: {
                type: Number,
                required: true
            }
        },
        data() {
            return {
                showEdit: false,
                updatedContent: '',
            };
        },
        methods: {
            editNoteContent() {
                this.updatedContent = this.note.content;
                this.showEdit = true;
            },
            async saveNote() {
                try {
                    const response = await fetch(`/Notes/UpdateNote/${this.note.noteId}`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({ content: this.updatedContent }),
                    });

                    if (response.ok) {
                        console.log('Note updated successfully');
                        this.note.content = this.updatedContent;
                        this.showEdit = false;
                    } else {
                        console.error('Failed to update note');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
        },
    };
</script>

<style scoped>
    .source-page {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .add-note-section {
        margin-top: 20px;
    }

    textarea {
        width: 100%;
        height: 100px;
        margin-bottom: 10px;
    }

    .notes-column {
        margin-top: 20px;
    }

    .note {
        margin-bottom: 10px;
    }
</style>
