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
                maxHeightStyle: '100px',
                maxWidthStyle: '200px',  
                maxTruncatedLength: 50,  
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
                    const response = await fetch(`/Notes/GetSourceByNoteId/${this.note.notesId}`);
                    if (response.ok) {
                        const source = await response.json();
                        if (source.sourceId) {
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
    };
</script>

<style scoped>
    .note-box {
        cursor: pointer;
        padding: 10px;
        border-radius: 10px; 
        background-color: #f0f0f0; 
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); 
        transition: transform 0.3s ease-in-out; 
    }

        .note-box:hover {
            transform: translateY(-5px);
        }

    .note-content {
        max-height: 100%; 
        overflow: hidden; 
        text-overflow: ellipsis; 
        white-space: nowrap;
    }
</style>
