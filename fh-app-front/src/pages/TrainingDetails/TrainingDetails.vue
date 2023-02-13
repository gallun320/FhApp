<template>
    <b-card title="Training details">
    <b-form @submit="onSubmit">
      <b-card 
        v-for="(training, counter) in trainings"
        :key="counter"
        >
        <b-button @click="add(counter)" variant="success">+</b-button>
        <b-form-group label-cols="4" label-cols-lg="2" :label="training.name" label-for="training-repeat">
          <b-form-input
            id="training-repeat"
            v-model="training.repeat"
            type="text"
            placeholder="Enter training repeat"
            required
          ></b-form-input>
        </b-form-group> 
        <br>
        <b-button @click="remove(counter)" variant="danger">-</b-button>
        
      </b-card >
      
      <br>
      
      <b-button-group>
          <b-button type="submit" variant="primary">Complete</b-button>
        </b-button-group>
    </b-form>
  </b-card>
</template>

<script>
export default {
    data() {
        return {
            trainings:[]
        };
    },
    mounted() {
        this.trainings = [{
                id: 0,
                name: "Test name",
                repeat: 0
            }];
    },
    methods: {
        remove(idx) {
            this.trainings[idx].repeat--;
        },
        add(idx) {
            this.trainings[idx].repeat++;
        }
    }
}
</script>