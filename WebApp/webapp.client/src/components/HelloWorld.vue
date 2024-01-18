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
        <!-- PDF location -->
        <div class="content">
            <h2>PDF Upload</h2>
            <input type="file" ref="fileInput" @change="handleFileUpload" accept=".pdf" />
            <button @click="uploadFile">Upload PDF</button>
        </div>
    </div>


</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                post: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
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

            async uploadFile() {
                const fileInput = this.$refs.fileInput;
                const file = fileInput.files[0];

                const formData = new FormData();
                formData.append('pdfFile', file);

                try {
                    const response = await fetch('File/uploadpdf', {
                        method: 'POST',
                        body: formData,
                    });

                    if (response.ok) {
                        const result = await response.json();
                        console.log(result);
                        // You can handle the result as needed (e.g., display a success message)
                    } else {
                        console.error('File upload failed');
                        console.log(response);
                    }
                } catch (error) {
                    console.error('Error uploading file:', error);
                }
            },
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