<template>
  <b-card title="Card title" sub-title="Card subtitle">
    <b-form @submit="onSubmit">
      <b-card 
        v-for="(counter, training) in trainings"
        v-bind:key="counter"
        >
        <b-form-group label-cols="4" label-cols-lg="2" label="Training name:" label-for="training-name">
          <b-form-input
            id="training-name"
            v-model="training.name"
            type="text"
            placeholder="Enter training name"
            required
          ></b-form-input>
        </b-form-group>

        <b-form-group label-cols="4" label-cols-lg="2" label="Training value:" label-for="training-value">
          <b-form-input
            id="training-value"
            v-model="training.value"
            type="text"
            placeholder="Enter training value"
            required
          ></b-form-input>
        </b-form-group> 
        <br>
        <b-button @click="remove(counter)" type="submit" variant="danger">Remove</b-button>
        
      </b-card >
      
      <br>
      
      <b-button-group>
          <b-button type="submit" variant="primary">Add</b-button>
          <b-button @click="complete()" variant="success">Complete</b-button>
        </b-button-group>
    </b-form>
  </b-card>
</template>

<script>
  export default {
    data() {
      return {
        trainings: [{
          name: "",
          value: ""
        }]
      }
    },
    methods: {
      onSubmit(e) {
        e.preventDefault();
        this.trainings.push({
          name:'',
          value: ''
        });
      },
      remove(counter) {
        this.trainings.splice(counter,1);
      },
      complete() {
        console.log(this.$route.params)
      }
    }
  }
</script>