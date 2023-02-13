<template>
  <b-container fluid="md">
    <b-card title="Add training">
        <Form @onTrainingAdd="handlerOnTrainingAdd"/>
    </b-card>
    <br>
    <h2>Trainings</h2>
    <br>
    <b-card :title="traning.name" v-for="traning in trainings">
      <b-card-text>
        Training repeat: {{ traning.repeat }}
      </b-card-text>
      <b-card-text>
        Training count in repeat: {{ traning.countInRepeat }}
      </b-card-text>
      <router-link :to="{ path: `/training-details/${traning.id}`}">Details</router-link>
    </b-card>
  </b-container>
</template>
<script setup>
    import Form from './components/FormAddTraining.vue';
    import { useRouter } from 'vue-router';
    const router = useRouter();

    const trainings = [{
      id: 1,
      name: "Test",
      repeat: 0,
      countInRepeat: 0
    }];

    const handlerOnTrainingAdd = (data) => {
       console.log(data);
       router.push({ path: `/training-add-details/${data.id}` });
    }
</script>
