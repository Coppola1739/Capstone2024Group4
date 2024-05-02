<template>
    <div class="source-page">
        <Navbar :userId="userIdFromQuery" />
        <div class="source-details" v-if="source">
            <div v-if="isVideoSource">
                <iframe width="560" height="315" :src="videoUrl" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
            </div>
            <div v-else class="pdf-viewer">
                <h1>{{ source.sourceName }}</h1>
                <iframe :src="pdfUrl" type="application/pdf" width="100%" height="800px"></iframe>
            </div>
            <div class="notes-section">
                <div class="add-note-section">
                    <h2>Add Note</h2>
                    <textarea v-model="newNoteContent" placeholder="Enter your note"></textarea>
                    <button @click="addNote">Add Note</button>
                </div>

                <div class="notes-column">
                    <h2>Notes</h2>
                    <notes-module v-for="note in notes" :key="note.notesId" :note="note" :note-id="note.notesId" :show-all-notes="showAllNotes" @note-updated="fetchNotes(id)"></notes-module>
                </div>
            </div>
        </div>
        <div v-else>
            <p>Loading...</p>
        </div>
    </div>
</template>

<script>
    import NotesModule from './NotesModule.vue';
    import Navbar from './NavbarModule.vue';
    
    export default {
        components: {
            Navbar,
            NotesModule,
        },
        props: {
            id: {
                type: [Number, String],
                required: true,
            },
            userId: {
                type: [Number, String],
                required: true,
            },
        },

        data() {
            return {
                pdfUrl: '',
                videoUrl: '',
                source: null,
                newNoteContent: '',
                notes: [],
                showAllNotes: false,
                userIdFromQuery: null,
            };
        },
        mounted() {
            this.userIdFromQuery = this.$route.query.userId;
            const sourceId = Number(this.id);
            this.fetchSourceDetails(sourceId)
                .then(() => {
                    if (this.source.sourceType === 'video') {
                        this.createVideoUrl();
                        this.fetchNotes(sourceId);
                    } else {
                        this.createPdfUrl();
                        this.fetchNotes(sourceId);
                    }
                })
                .catch(error => {
                    console.error('Error fetching source details:', error);
                });

            this.fetchNotes(sourceId);
        },
        watch: {
            source: {
                immediate: true,
                handler(newValue) { 
                    if (newValue) {
                        this.isVideoSource = this.source && this.source.sourceType === 'video';
                    }
                }
            }
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
            async createVideoUrl() {
                if (this.source && this.source.content) {
                    if (typeof this.source.content === 'string') {
                        const decodedUrl = atob(this.source.content);
                        this.videoUrl = decodedUrl.replace("watch?v=", "embed/");
                        console.log(this.videoUrl)
                    } else {
                        console.error('Invalid content format');
                    }
                } else {
                    console.error('Source or content is missing');
                }
            },

            async createPdfUrl() {
                if (this.source && this.source.content) {
                    const pdfSource = atob(this.source.content);
                    const dataArray = new Uint8Array(pdfSource.length);
                    for (let i = 0; i < pdfSource.length; i++) {
                        dataArray[i] = pdfSource.charCodeAt(i);
                    }

                    const blob = new Blob([dataArray], { type: 'application/pdf' });
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
            toggleShowAllNotes() {
                this.showAllNotes = !this.showAllNotes;
            },
        },
    };
</script>

<style scoped>
    .source-page {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: space-between;
    }
        .source-page button {
            background-color: #007bff;
            color: white;
            border-radius: 5%;
            border: 1px solid #007bff;
        }

        .notes-section{
            margin-right: -40%;
        }

    .pdf-viewer {
        width: 100%;
        height: 100vh;
    }

    .source-details {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
    }

    .notes-column {
        display: flex;
        flex-direction: column;
        align-items: start;
        margin-left: 20%;
        margin-top: 5%;
        width: 100%; 
    }

    .add-note-section {
        width: 100%;
        margin-left: 20%;
        margin-top: 5%;
    }

    textarea {
        width: 100%;
        height: 70%;
        margin-bottom: 3%;
    }

    .note {
        margin-bottom: 5%;
    }
    @media screen and (min-width: 1020px) {
        .box {
            padding: 10%;
            margin-left: 90%;
            max-width: 70%;
        }
    }
</style>
