<template>
  <div class="q-pa-md row justify-center">
    <div style="width: 100%; max-width: 400px">
      <div style="width: 100%;" v-for="(message, index) in messages" :key="index">
        <q-chat-message
          avatar="../assets/icon.png"
          :text="[message.body]"  
          sent
          :name="message.name"
          :stamp="message.date"
          v-if="message.name == userActual"
        />
        <q-chat-message v-else
          avatar="../assets/icon.png"
          :text="[message.body]" 
          :name="message.name"
          :stamp="message.date"
          bg-color="amber-7"
        />
      </div>
    </div>
  </div>
</template>

<style>
  .q-chat-message {
    margin-bottom: 8px;
  }
</style>


<script>
export default {
  name: 'ChatComponent',
  props: {
    userActual: String,
    messages: Array,
  },
};
</script>
