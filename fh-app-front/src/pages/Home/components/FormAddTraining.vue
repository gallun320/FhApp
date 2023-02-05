<template>
    <b-form @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group
        id="training-name-group"
        label="Training name:"
        label-for="training-name"
        description="Place any name that would you like"
      >
        <b-form-input
          id="training-name"
          v-model="form.trainingName"
          type="text"
          placeholder="Enter training name"
          required
        ></b-form-input>
      </b-form-group>

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