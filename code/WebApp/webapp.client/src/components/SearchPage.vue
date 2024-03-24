<template>
    <div>
        <div class="search-container">
            <input type="text" v-model="searchInput" placeholder="Search notes">
            <button @click="searchNotes">Search</button>
        </div>
        <ul class="note-list">
            <NoteModuleVue v-for="(note, index) in notes" :key="index" :note="note" @note-updated="fetchNotes" />
        </ul>
    </div>
</template>

<script>
    import NoteModuleVue from './SearchNotesModule.vue';
    //TODO: Make new notemodule for search results to display. When clicked on, it will take you to source

    export default {
        components: {
            NoteModuleVue
        },
        data() {
            return {
                searchInput: '',
                notes: [],
            };
        },
        methods: {
            async searchNotes() {
                try {
                    const response = await fetch(`/Notes/GetNotesByTag/${this.searchInput}`);
                    if (response.ok) {
                        this.notes = await response.json();
                    } else {
                        console.error('Failed to fetch notes');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
            async fetchNotes() {
                // Fetch notes when needed, like after a note is updated or deleted
                await this.searchNotes();
            }
        }
    };
</script>

<style scoped>
    .search-container {
        text-align: center;
        margin-bottom: 20px;
    }

        .search-container input[type="text"] {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .search-container button {
            padding: 10px 20px;
            border: 1px solid #007bff;
            border-radius: 5px;
            background-color: #007bff;
            color: white;
            cursor: pointer;
        }

            .search-container button:hover {
                background-color: #0056b3;
            }

    .note-list {
        list-style-type: none;
        padding: 0;
    }
</style>
