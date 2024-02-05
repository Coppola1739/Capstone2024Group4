<template>
    <div class="source-page">
        <div class="source-details" v-if="source">
            <h1>{{ source.sourceName }}</h1>
            <!-- Display other source details here -->
            <!-- Add notes section -->
            <div class="add-note-section">
                <h2>Add Note</h2>
                <textarea v-model="newNoteContent" placeholder="Enter your note"></textarea>
                <button @click="addNote">Add Note</button>
            </div>

            <!-- Display existing notes -->
            <div class="notes-column">
                <h2>Notes</h2>
                <div class="note" v-for="note in notes" :key="note.noteId">
                    <p>{{ note.content }}</p>
                </div>
            </div>
        </div>
        <div v-else>
            <p>Loading...</p>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            id: {
                type: [Number, String],
                required: true,
            },
        },
        data() {
            return {
                source: null,
                newNoteContent: '',
                notes: [],
            };
        },
        mounted() {
            const sourceId = Number(this.id);
            //this.fetchSourceDetails(sourceId);
            this.source = {
                sourceId: sourceId,
                sourceName: "test",
                uploadDate: "2024-02-03T21:36:04.1950091"
            };
            this.fetchNotes(sourceId);
        },
        methods: {
            async fetchSourceDetails(id) {
                try {
                    const response = await fetch(`File/GetSourceById?id=${id}`);
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

                    const response = await fetch(`Notes/GetNotesBySourceId/${sourceId}`);
                    console.log(`Notes/GetNotesBySourceId/${sourceId}`)
                    console.log(response)
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
            async addNote() {
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
