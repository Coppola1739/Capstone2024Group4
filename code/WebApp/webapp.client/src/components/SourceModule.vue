<template>
    <router-link :to="{ name: 'SourcePage', params: { id: 4 }}">
        <div class="source-module">
            <h3>{{ sourceName }}</h3>
            <p>Uploaded on: {{ uploadDate }}</p>
        </div>
    </router-link>
</template>

<script>
export default {
  props: {
    sourceId: Number,
    sourceName: String,
    uploadDate: String,
  },
  methods: {
  },
};
</script>

<style scoped>
    .source-module {
        border: 1px solid #ccc;
        border-radius: 4px;
        padding: 10px;
        cursor: pointer;
        margin-bottom: 10px;
    }
</style>
