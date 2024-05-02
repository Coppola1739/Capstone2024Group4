<template>
    <div class="box">
        <div class="header-info">
            <h1>Capstone Project</h1>
            <Navbar :userId="userId" />
        </div>

        <div class="source-content">
            <div class="upload-box">
                <div class="pdf-upload-section">
                    <h2>Source Upload</h2>
                    <select v-model="selectedFileType">
                        <option value="pdf">PDF</option>
                        <option value="video">Video</option>
                    </select>
                    <div v-if="selectedFileType === 'pdf'">
                        <input type="file" ref="fileInput" @change="handleFileUpload" accept=".pdf" />
                    </div>
                    <div v-else-if="selectedFileType === 'video'">
                        <textarea v-model="videoLink" placeholder="Enter video link"></textarea>
                        <button @click="confirmVideoLink">OK</button>
                    </div>
                    <form v-if="showForm">
                        <div class="form-group">
                            <label for="sourceName">Source Name:</label>
                            <input type="text" id="sourceName" v-model="formData.sourceName" required />
                        </div>

                        <div class="form-group">
                            <label for="authorFirstName">Author First Name:</label>
                            <input type="text" id="authorFirstName" v-model="formData.authorFirstName" />
                        </div>

                        <div class="form-group">
                            <label for="authorLastName">Author Last Name:</label>
                            <input type="text" id="authorLastName" v-model="formData.authorLastName" />
                        </div>

                        <div class="form-group">
                            <label for="title">Title:</label>
                            <input type="text" id="title" v-model="formData.title" />
                        </div>

                        <button type="button" @click="submitForm">Submit</button>
                        <button type="button" @click="cancelUpload">Cancel</button>
                    </form>
                </div>
            </div>

            <div class="uploaded-sources-box">
                <div class="source-modules-column">
                    <h2>Uploaded Sources</h2>
                    <SourceModule v-for="source in userSources"
                                  :key="source.sourceId"
                                  :sourceId="source.sourceId"
                                  :sourceName="source.sourceName"
                                  :uploadDate="source.uploadDate"
                                  @source-deleted="handleSourceDeleted" 
                                  :userId="userId" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue';
    import Navbar from './NavbarModule.vue';
    import SourceModule from './SourceModule.vue';

    export default defineComponent({
        components: {
            Navbar,
            SourceModule,
        },
        data() {
            return {
                loading: false,
                userId: null,
                userSources: [],

                post: null,
                showForm: false,
                formData: {
                    sourceName: '',
                    authorFirstName: '',
                    authorLastName: '',
                    title: '',
                    sourceType: ''
                },
                selectedFileType: 'pdf',
                videoLink: '',
            };
        },
        methods: {
            async fetchUserSources() {
                this.loading = true;
                try {
                    const response = await fetch(`File/GetUsersSources?userId=${this.userId}`);
                    if (response.ok) {
                        const data = await response.json();
                        this.userSources = data.sources;
                    } else {
                        console.error('Failed to fetch user sources');
                    }
                } catch (error) {
                    console.error('Error fetching user sources:', error);
                } finally {
                    this.loading = false;
                }
            },
            cancelUpload() {
                this.formData.sourceName = '';
                this.formData.authorFirstName = '';
                this.formData.authorLastName = '';
                this.formData.title = '';
                this.selectedFileType = 'pdf';
                this.videoLink = '';
                this.showForm = false;
                if (this.$refs.fileInput) {
                    this.$refs.fileInput.value = '';
                }
            },
            async handleSourceDeleted(deletedSourceId) {
                await this.fetchUserSources();
            },
            fetchData() {
                this.post = null;
                this.loading = true;
            },
            handleFileUpload() {
                this.showForm = true;

                if (this.selectedFileType === 'pdf') {
                    const fileName = this.$refs.fileInput.files[0].name;
                    const fileType = fileName.split('.').pop().toUpperCase();
                    this.formData.sourceType = fileType;
                } else if (this.selectedFileType === 'video') {

                }
            },
            confirmVideoLink() {
                this.showForm = true;
                this.selectedFileType = "video"
                this.formData.sourceType = 'video';
            },
            async submitForm() {
                const formData = new FormData();
                if (this.selectedFileType === 'pdf') {
                    formData.append('pdfFile', this.$refs.fileInput.files[0]);
                } else if (this.selectedFileType === 'video') {
                    formData.append('videolink', this.videoLink);
                }
                formData.append('userId', this.userId);
                formData.append('sourceName', this.formData.sourceName);
                formData.append('authorFirstName', this.formData.authorFirstName);
                formData.append('authorLastName', this.formData.authorLastName);
                formData.append('title', this.formData.title);
                formData.append('sourceType', this.formData.sourceType);
                if (this.$refs.fileInput) {
                    this.$refs.fileInput.value = '';
                }

                this.showForm = false;

                try {
                    let response;
                    if (this.selectedFileType === 'video') {
                         response = await fetch('File/uploadvideo', {
                            method: 'POST',
                            body: formData,
                        });
                    }
                    else if (this.selectedFileType === 'pdf') {
                         response = await fetch('File/uploadpdf', {
                            method: 'POST',
                            body: formData,
                        });
                    }
                    if (response.ok) {
                        const result = await response.json();
                        alert('Source uploaded successfully');
                        window.location.reload();
                    } else {
                        alert('File upload failed' + response);
                    }
                } catch (error) {
                    console.error('Error uploading file:', error);
                    alert('Error uploading file: ' + error.message);
                }
            },
        },
        created() {
            this.userId = this.$route.query.userId;
            this.fetchData();
            this.fetchUserSources();
        },
    });
</script>


<style scoped>
    .box {
        display: flex;
        flex-direction: column;
        justify-content: center;
        padding: 0;
        width: 100%;
        height: 100%;
    }
    .header-info {
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 30px;
        background-color: #f0f0f0;
    }
    .source-content {
        display: flex;
        flex-direction: row-reverse;
    }

    .upload-box {
        display: flex;
        justify-content: space-between;
        flex-direction: column;
    }

    .uploaded-sources-box {
        display: flex;
        justify-content: space-between;
        flex-direction: column;
        padding-right: 30%;
    }

    .pdf-upload-section {
        margin-top: 10%;
        margin-left: 20%
    }

    .pdf-upload-section form {
        display: flex;
        flex-direction: column;
        margin: auto;
    }

    .source-modules-column {
        flex: content;
    }
    .source-modules-column .delete-icon {
            pointer-events: auto; 
    }

    th {
        font-weight: bold;
    }

    tr:nth-child(even) {
        background: #F2F2F2;
    }

    tr:nth-child(odd) {
        background: #FFF;
    }

    th, td {
        padding-left: .5rem;
        padding-right: .5rem;
    }

    table {
        margin-left: auto;
        margin-right: auto;
    }

    .form-group {
        margin-bottom: 15px;
    }

    label {
        font-weight: bold;
    }

    input[type="text"] {
        width: 100%;
        padding: 8px;
        border: 1px solid #ccc;
        border-radius: 4px;
    }

    button {
        padding: 10px 20px;
        background-color: #007bff;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

        button:hover {
            background-color: #0056b3;
        }

    @media screen and (min-width: 1020px) {
        .box{
            padding: 10%;
            margin-left:90%;
            max-width: 70%;
        }
    }
</style>