<template>
    <div class="source-page">
        <div class="source-details" v-if="source">
            <h1>{{ source.sourceName }}</h1>
            <div class="pdf-viewer">
                <iframe :src="pdfUrl" type="application/pdf" width="100%" height="600px"></iframe>
            </div>

            <div class="add-note-section">
                <h2>Add Note</h2>
                <textarea v-model="newNoteContent" placeholder="Enter your note"></textarea>
                <button @click="addNote">Add Note</button>
            </div>

            <div class="notes-column">
                <h2>Notes</h2>
                <notes-module v-for="note in notes" :key="note.noteId" :note="note" :note-id="note.noteId" @note-updated="updateNote"></notes-module>
            </div>

        </div>
        <div v-else>
            <p>Loading...</p>
        </div>
    </div>
</template>

<script>
    import NotesModule from './NotesModule.vue';

    export default {
        components: {
            NotesModule,
        },
        props: {
            id: {
                type: [Number, String],
                required: true,
            },
        },
        data() {
            return {
                pdfUrl: '',
                source: null,
                newNoteContent: '',
                notes: [],
            };
        },
        mounted() {
            const sourceId = Number(this.id);
            this.fetchSourceDetails(sourceId)
                .then(() => {
                    this.createPdfUrl();
                    this.fetchNotes(sourceId);
                })
                .catch(error => {
                    console.error('Error fetching source details:', error);
                });

            this.fetchNotes(sourceId);
        },
        methods: {
            async fetchSourceDetails(id) {
                try {
                    const response = await fetch(`/File/GetSourceById?id=${id}`);
                    if (response.ok) {
                        const data = await response.json();
                        this.source = data;
                    } else {
                        console.error('Failed to fetch source details');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
            async fetchNotes(sourceId) {
                try {
                    const response = await fetch(`/Notes/GetNotesBySourceId/${sourceId}`);
                    if (response.ok) {
                        const data = await response.json();
                        this.notes = data;
                    } else {
                        console.error('Failed to fetch notes');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
            async createPdfUrl() {
                if (this.source && this.source.content) {
                    const pdfSource = atob(this.source.content);
                    const dataArray = new Uint8Array(pdfSource.length);
                    for (let i = 0; i < pdfSource.length; i++) {
                        dataArray[i] = pdfSource.charCodeAt(i);
                    }
                    // Create Blob object from Uint8Array
                    const blob = new Blob([dataArray], { type: 'application/pdf' });

                    // Create URL for the Blob object
                    this.pdfUrl = URL.createObjectURL(blob);
                } else {
                    console.error('Source or content is missing');
                }
            },
            async addNote() {
                const formData = new FormData();
                formData.append('sourceId', this.id);
                formData.append('content', this.newNoteContent);
                console.log(this.id);
                console.log(this.newNoteContent);
                try { 
                    const response = await fetch('/Notes/AddNote', {
                        method: 'POST',
                        body: formData,
                    });

                    if (response.ok) {
                        console.log('Note added successfully');
                        this.newNoteContent = '';
                        this.fetchNotes(this.id);
                    } else {
                        console.error('Failed to add note');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
            async updateNote() {
                this.fetchNotes(this.id);
            },
        },
    };
</script>

<style scoped>
    .pdf-viewer {
        width: 100%;
        height: 100%;
    }
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
