<template>
    <div>
        <div class="search-container">
            <div class="add-filter">
                <input type="text" v-model="searchInput" @input="fetchTagSuggestions" @keydown.enter="addFilter" placeholder="Search notes">
                <button @click="addFilter">Add Filter</button>
                <div v-if="tagSuggestions.length > 0" class="tag-suggestions">
                    <ul>
                        <li v-for="tag in tagSuggestions" :key="tag" @click="selectTag(tag)">{{ tag }}</li>
                    </ul>
                </div>
            </div>
            <div class="filter-list">
                <div v-for="(filter, index) in appliedFilters" :key="index" class="filter-item">
                    {{ filter }}
                    <button @click="removeFilter(index)">x</button>
                </div>
            </div>
            <div class="search-button">
                <button @click="searchNotes">Search</button>
            </div>
        </div>
        <ul class="note-list">
            <NoteModuleVue v-for="(note, index) in notes" :key="index" :note="note" @note-updated="fetchNotes" />
        </ul>
    </div>
</template>

<script>
    import NoteModuleVue from './SearchNotesModule.vue';

    export default {
        components: {
            NoteModuleVue
        },
        data() {
            return {
                searchInput: '',
                notes: [],
                appliedFilters: [],
                tagSuggestions: []
            };
        },
        methods: {
            async fetchTagSuggestions() {
                try {
                    console.log(this.searchInput)
                    const response = await fetch(`/Tag/SearchTags?query=${this.searchInput}`);
                    if (response.ok) {
                        this.tagSuggestions = await response.json();
                    } else {
                        console.error('Failed to fetch tag suggestions');
                    }
                } catch (error) {
                    console.error('Error fetching tag suggestions', error);
                }
            },
            async searchNotes() {
                try {
                    const response = await fetch(`/Notes/GetNotesByTags`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.appliedFilters)
                    });
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
                await this.searchNotes();
            },
            addFilter() {
                if (this.searchInput.trim() !== '' && !this.appliedFilters.includes(this.searchInput.trim())) {
                    this.appliedFilters.push(this.searchInput.trim());
                    this.searchInput = '';
                }
            },
            removeFilter(index) {
                this.appliedFilters.splice(index, 1);
            },
            selectTag(tag) {
                this.appliedFilters.push(tag);
                this.searchInput = '';
                this.tagSuggestions = []; 
            }
        }
    };
</script>

<style scoped>
    .search-container {
        display: flex;
        flex-direction: column;
        text-align: center;
        margin-bottom: 20px;
    }


        .search-container input[type="text"] {
            padding: 10px;
            margin: 2%;
            border: 20% solid #ccc;
            border-radius: 5%;
        }

        .search-container button {
            padding: 10px 20px;
            border: 1px solid #007bff;
            border-radius: 3%;
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

    .filter-list {
        margin-top: 10px;
    }
    li {
        list-style-type: none;
        padding: 10px;
        background-color: #f0f0f0;
        transition: background-color 0.3s;
    }

    li :hover{
        background-color: azure;
        cursor: pointer;
    }
    .filter-item {
        display: inline-block;
        margin-right: 5px;
        background-color: #f0f0f0;
        padding: 5px 10px;
        border-radius: 5px;
    }

        .filter-item button {
            margin-left: 5px;
            cursor: pointer;
            background-color: #ccc;
            border: none;
            border-radius: 90%;
            height: 25px;
            line-height: 0;
            text-align: center;
        }
</style>
