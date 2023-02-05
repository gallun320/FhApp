<template>
  <b-container fluid="md">
    <b-row id="training-form-integration">
      <b-col>
        <div>
            <Form @onTrainingAdd="handlerOnTrainingAdd"/>
        </div>
      </b-col>
    </b-row>
    <b-row class="justify-content-md-center">
      <b-col cols="12" md="auto">
        <h1>Home page</h1>
      </b-col>
    </b-row>
  </b-container>
</template>
<script setup>
    import Form from './components/FormAddTraining.vue';

    const handlerOnTrainingAdd = (data) => {
       console.log(data);
    }
</script>
