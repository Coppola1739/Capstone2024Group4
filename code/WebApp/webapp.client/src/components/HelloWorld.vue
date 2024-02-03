<template>
    <div class="title-component">
        <h1>Capstone Project</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Username</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="user in post" :key="user.id">
                        <td>{{ user.id }}</td>
                        <td>{{ user.username }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
 
        <div class="pdf-upload-section">
            <h2>PDF Upload</h2>
            <input type="file" ref="fileInput" @change="handleFileUpload" accept=".pdf" />
            <form v-if="showForm">
                <label for="sourceName">Source Name:</label>
                <input type="text" id="sourceName" v-model="formData.sourceName" required />

                <label for="authorFirstName">Author First Name:</label>
                <input type="text" id="authorFirstName" v-model="formData.authorFirstName" />

                <label for="authorLastName">Author Last Name:</label>
                <input type="text" id="authorLastName" v-model="formData.authorLastName" />

                <label for="title">Title:</label>
                <input type="text" id="title" v-model="formData.title" />

                <button type="button" @click="submitForm">Submit</button>
            </form>
        </div>
    </div>

</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
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

                this.$refs.fileInput.value = ''; // Reset file input
                this.showForm = false;

                try {
                    const response = await fetch('File/uploadpdf', {
                        method: 'POST',
                        body: formData,
                    });

                    if (response.ok) {
                        const result = await response.json();
                        // Display success message using alert or custom modal
                        alert('PDF uploaded successfully');
                    } else {
                        console.error('File upload failed');
                        // Display error message using alert or custom modal
                        alert('File upload failed');
                        console.log(response);
                    }
                } catch (error) {
                    console.error('Error uploading file:', error);
                    // Display error message using alert or custom modal
                    alert('Error uploading file: ' + error.message);
                }
            },
        },
        created() {
            this.fetchData();
        },
    });
</script>


<style scoped>
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
</style>