<template>
  <q-layout view="lHh Lpr lFf" class="app-layout">
    <q-page-container class="no-scroll">
      <div class="header-container">
        <q-input
          outlined
          placeholder="Informe seu Nome"
          v-model="message.name"
          class="q-pa-sm input-custom"
          dense
        >
          <template v-slot:before>
            <q-label class="label-small label-white">Nome</q-label>
          </template>
        </q-input>
      </div>

      <div class="message-container" ref="messageContainer">
        <ChatComponent :messages="messages" :userActual="message.name" />
      </div>

      <div class="footer-container">
        <q-input
          outlined
          @keyup.enter="send"
          placeholder="Digite..."
          v-model="message.body"
          class="q-pa-sm text-center input-custom"
          dense
        >
          <template v-slot:before>
            <q-label class="label-small label-white">Mensagem</q-label>
          </template>
          <template v-slot:append>
            <q-icon v-if="message.body !== ''" aria-setsize="sm" name="send" class="icon-send"></q-icon>
          </template>
        </q-input>
      </div>
    </q-page-container>
  </q-layout>
</template>

<script>
import { ref, reactive, onMounted, onBeforeUnmount } from 'vue';
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

    const messageContainerRef = ref(null);

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

            // Rola para a última mensagem se a referência existir
            if (messageContainerRef.value) {
              messageContainerRef.value.scrollTop = messageContainerRef.value.scrollHeight;
            }
          });
        })
        .catch((e) => console.log('Connection Failed', e));
    });

    onBeforeUnmount(() => {
    });

    return {
      send,
      messages,
      message,
      messageContainerRef,
    };
  },
};
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=JetBrains+Mono&display=swap');

  .app-layout {
    background: #1e1e1e;
    color: #fff;
font-family: 'JetBrains Mono', monospace;
  }
  .q-layout {
    background-color: #1e1e1e;
  }
  .no-scroll {
    overflow: hidden;
    height: 100vh;
  }
  .header-container, .footer-container {
    background-color: #2d2d2d;
    padding: 1rem;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
    text-align: center;
    color: #fff;
  }
  .input-custom {
    background-color: #383838;
    border-radius: 8px;
    transition: background-color 0.3s;
    color: #fff;
  }
  .input-custom:focus {
    background-color: #525252;
    color: #fff;
  }
  .label-small {
    font-size: 12px;
    color: #fff;
  }
  .label-white {
    color: #fff;
  }
  .message-container {
    overflow-y: auto;
    max-height: calc(100vh - 2 * 56px - 3rem);
    padding: 1rem;
    color: #fff;
  }
  .footer-container {
    position: fixed;
    bottom: 0;
    width: 100%;
    box-shadow: 0 -2px 4px rgba(0, 0, 0, 0.2);
    color: #fff;
  }
  .text-center {
    text-align: center;
    color: #fff;
  }
  .icon-send {
    color: #fff;
    cursor: pointer;
    transition: color 0.3s;
    color: #fff;
  }
  .icon-send:hover {
    color: #ddd;
    color: #fff;
  }

  .q-input {
    color: rgb(255 255 255 / 87%);
  }
</style>
