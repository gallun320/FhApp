<template>
  <b-card title="Training add details">
    <b-form @submit="onSubmit">
      <b-card 
        style="margin-top: 10px;"
        v-for="(training, counter) in trainings"
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

        <b-form-group label-cols="4" label-cols-lg="2" label="Training repeat:" label-for="training-repeat">
          <b-form-input
            id="training-repeat"
            v-model="training.repeat"
            type="text"
            placeholder="Enter training repeat"
            required
          ></b-form-input>
        </b-form-group> 

        <b-form-group label-cols="4" label-cols-lg="2" label="Training count in repeat:" label-for="training-count-in-repeat">
          <b-form-input
            id="training-count-in-repeat"
            v-model="training.countInRepeat"
            type="text"
            placeholder="Enter training count in repeat"
            required
          ></b-form-input>
        </b-form-group> 
        <br>
        <b-button @click="remove(counter)" variant="danger">Remove</b-button>
        
      </b-card >
      
      <br>
      
      <b-button-group>
          <b-button type="submit" variant="primary">Complete</b-button>
          <b-button @click="add()" variant="success">Add</b-button>
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
          repeat: 0,
          countInRepeat: 0
        }]
      }
    },
    methods: {
      onSubmit(e) {
        e.preventDefault();
        console.log(this.$route.params)
      },
      remove(counter) {
        this.trainings.splice(counter,1);
      },
      add() {
        this.trainings.push({
          name:'',
          repeat: 0,
          countInRepeat: 0
        });
      }
    }
  }
</script>