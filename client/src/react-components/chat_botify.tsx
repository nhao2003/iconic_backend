import React from "react";
import ChatBot, { Flow, Params } from "react-chatbotify";
import { marked } from "marked";

const ChatBotify = () => {

  async function callApi(params: Params) {
    const response = await fetch("http://localhost:8683/generate", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        "query": params.userInput,
      })
    });
    const data = await response.json();
    //  Thay nhiều dấu\n bằng 1 \n
    const responseMessage = data.response.replace(/\n{2,}/g, '\n');
    params.injectMessage(marked(responseMessage));
  }

  const flow: Flow = {
    "start": {
      message: "Xin chào, tôi là một chatbot. Bạn cần giúp gì?",
      path: "loop"
    },
    "loop": {
      message: callApi,
      path: "loop"
    }
  };

  return (
    <ChatBot
      flow={flow}
      plugins={[]}
      settings={
        {
          botBubble: {
            dangerouslySetInnerHtml: true
          },
          header: {
            title: "Iconic - Trợ lý ảo",
          },
          tooltip: {
            text: "Iconic - Trợ lý ảo",
          },
        }
      }
    />
  );
};

export default ChatBotify;
