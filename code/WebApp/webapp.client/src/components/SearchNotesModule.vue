<template>
    <div class="note-box" @click="navigateToSource">
        <div class="note-content" :style="{ maxHeight: maxHeightStyle, maxWidth: maxWidthStyle }">
            {{ isTruncated ? truncatedContent + '...' : note.content }}
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
        },
        data() {
            return {
                maxHeightStyle: '100px', // Maximum height of the note content area
                maxWidthStyle: '200px',  // Maximum width of the note content area
                maxTruncatedLength: 50,   // Maximum length before truncation
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
            async navigateToSource() {
                try {
                    console.log(this.note.notesId)
                    const response = await fetch(`/Notes/GetSourceByNoteId/${this.note.notesId}`);
                    if (response.ok) {
                        const source = await response.json();
                        console.log(source)
                        if (source.sourceId) {
                            // Navigate to the source page using router-link
                            console.log(source.id)
                            this.$router.push({ name: 'SourcePage', params: { id: source.sourceId } });
                        } else {
                            console.error('Source not found for the provided note ID');
                        }
                    } else {
                        console.error('Failed to fetch source for the note');
                    }
                } catch (error) {
                    console.error('Error', error);
                }
            },
            },
            getSourcePageUrl(sourceId) {
                // Implement this method to get the source page URL based on the note ID
                // Example:
                // return `/source/${sourceId}`;
            },
    };
</script>

<style scoped>
    .note-box {
        cursor: pointer; /* Add cursor pointer for clickable effect */
        padding: 10px; /* Add padding */
        border-radius: 10px; /* Add border-radius for rounded corners */
        background-color: #f0f0f0; /* Add background color */
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Add box shadow for depth */
        transition: transform 0.3s ease-in-out; /* Add transition effect */
    }

        .note-box:hover {
            transform: translateY(-5px); /* Add hover effect by translating slightly upwards */
        }

    .note-content {
        max-height: 100%; /* Set maximum height to 100% */
        overflow: hidden; /* Hide overflow content */
        text-overflow: ellipsis; /* Show ellipsis for overflow content */
        white-space: nowrap; /* Prevent wrapping */
    }
</style>
