<template>
  <q-layout view="lHh Lpr lFf">
    <q-page-container class="q-mb-md no-scroll">
      <div class="q-mb-md" style="position: sticky; top: 0; background-color: white; z-index: 1;">
        <q-input
          outlined
          placeholder="Informe seu Nome"
          v-model="message.name"
          class="q-pa-sm"
          dense
        ></q-input>
      </div>

      <div class="message-container">
        <ChatComponent :messages="messages" :userActual="message.name" />
      </div>

      <div class="q-mt-xl" style="position: sticky; bottom: 0; background-color: white; z-index: 1;">
        <q-input
          outlined
          @keyup.enter="send"
          placeholder="Digite sua mensagem"
          v-model="message.body"
          class="q-pa-sm"
          dense
        >
          <template v-slot:append>
            <q-icon aria-setsize="sm" name="arrow_circle_right"></q-icon>
          </template>
        </q-input>
      </div>
    </q-page-container>
  </q-layout>
</template>

<style scoped>
  .q-layout {
    background-color: #ffffff;
    text-emphasis: none;
  }

  .no-scroll {
    overflow: hidden;
    height: 100vh;
  }

  .message-container {
    overflow-y: auto;
    max-height: calc(100vh - 2 * 56px); 
  }
</style>

<script>
import { ref, reactive, onMounted } from 'vue';
import ChatComponent from './components/ChatComponent.vue';
import Hub from './Hub';

export default {
  name: 'LayoutDefault',

  components: {
    ChatComponent,
  },

  setup() {
    let messages = ref([]);
    let message = reactive({
      name: '',
      body: '',
    });
    let _hub = new Hub();

    function send() {
      if (message.body === '') return;
      _hub.connection.invoke('SendMessage', message);
      message.body = '';
    }

    onMounted(() => {
      _hub.connection
        .start()
        .then(() => {
          console.log('Connected');
          _hub.connection.on('ReceivedMessage', (msg) => {
            console.log(msg);
            messages.value.push(msg);
            console.log(messages.value);
          });
        })
        .catch((e) => console.log('Connection Failed', e));
    });

    return {
      send,
      messages,
      message,
    };
  },
};
</script>
