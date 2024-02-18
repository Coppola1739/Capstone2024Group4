<template>
    <div class="note">
        <div v-if="!showEdit">
            <div class="note-content">
                {{ isTruncated ? truncatedContent + '...' : note.content }}
            </div>
            <button @click="toggleEdit">View</button>
        </div>
        <div v-else>
            <textarea v-model="updatedContent"></textarea>
            <button @click="saveNote">Done</button>
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
                    this.updatedContent = this.note.content; // Set note content in textarea when entering edit mode
                }
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
                        this.showEdit = false;
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
        },
    };
</script>

<style scoped>
    .note-content {
        max-height: 2%;
        overflow: initial;
        text-overflow: ellipsis;
        white-space: nowrap;
    }
</style>
