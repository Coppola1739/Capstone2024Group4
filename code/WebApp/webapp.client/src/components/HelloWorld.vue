<template>
    <div class="title-component">
        <h1>Capstone Project</h1>
    </div>
    <div class="box">
        <div class="upload-box">
            <div class="pdf-upload-section">
                <h2>Source Upload</h2>
                <input type="file" ref="fileInput" @change="handleFileUpload" accept=".pdf" />
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
                              :uploadDate="source.uploadDate" />
            </div>
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import SourceModule from './SourceModule.vue';

    export default defineComponent({
        components: {
            SourceModule,
        },
        data() {
            return {
                loading: false,
                userSources: [],
                post: null,
                showForm: false,
                formData: {
                    sourceName: '',
                    authorFirstName: '',
                    authorLastName: '',
                    title: '',
                    sourceType: ''
                }
            };
        },
        methods: {
            async fetchUserSources() {
                this.loading = true;
                try {
                    const response = await fetch('File/GetUsersSources');
                    console.log(response);
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
            fetchData() {
                this.post = null;
                this.loading = true;

                fetch('User/getallusers')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });
            },
            handleFileUpload() {
                this.showForm = true;

                const fileName = this.$refs.fileInput.files[0].name;
                const fileType = fileName.split('.').pop().toUpperCase();
                this.formData.sourceType = fileType;
            },
            async submitForm() {
                const formData = new FormData();
                formData.append('pdfFile', this.$refs.fileInput.files[0]);
                formData.append('sourceName', this.formData.sourceName);
                formData.append('authorFirstName', this.formData.authorFirstName);
                formData.append('authorLastName', this.formData.authorLastName);
                formData.append('title', this.formData.title);
                formData.append('sourceType', this.formData.sourceType);

                this.$refs.fileInput.value = '';
                this.showForm = false;

                try {
                    const response = await fetch('File/uploadpdf', {
                        method: 'POST',
                        body: formData,
                    });

                    if (response.ok) {
                        const result = await response.json();
                        alert('PDF uploaded successfully');
                    } else {
                        console.error('File upload failed');
                        alert('File upload failed');
                        console.log(response);
                    }
                } catch (error) {
                    console.error('Error uploading file:', error);
                    alert('Error uploading file: ' + error.message);
                }
            },
        },
        created() {
            this.fetchData();
            this.fetchUserSources(); // Call the new method to fetch user sources
        },
    });
</script>


<style scoped>
    .box {
        display: flex;
        flex-direction: row-reverse;
        justify-content: space-around;
    }
    .upload-box{
        display:flex;
        justify-content: space-between;
        flex-direction: column;
    }
    .uploaded-sources-box{
        display: flex;
        justify-content:space-between;
        flex-direction: column;
    }
    .pdf-upload-section {
        margin-top: 20px;
    }
    .pdf-upload-section form {
            display: flex;
            flex-direction: column;
            margin: auto;
    }
    .source-modules-column {
        flex: content;
        margin-left: 20px;
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
    .title-component {
        text-align: center;
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
</style>