<template>
    <b-form @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group
        label-cols="4" label-cols-lg="2"
        id="training-name-group"
        label="Training name:"
        label-for="training-name"
      >
        <b-form-input
          id="training-name"
          v-model="form.trainingName"
          type="text"
          placeholder="Enter training name"
          required
        ></b-form-input>
      </b-form-group>
      <br>
      <b-button type="submit" variant="primary">Submit</b-button>
    </b-form>
</template>

<script>
  export default {
    emits: ['onTrainingAdd'],
    data() {
      return {
        form: {
          trainingName: '',
        },
        show: true
      }
    },
    methods: {
      onSubmit(event) {
        event.preventDefault()
        this.$emit("onTrainingAdd", { id: 1 });
      }
    }
  }
</script>