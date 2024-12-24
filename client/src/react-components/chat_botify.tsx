import React, { useRef, useState } from "react";
import ChatBot, { Flow, Params } from "react-chatbotify";
import { marked } from "marked";
import FaqAccordion from "./Accordion";
import ProductList from "./ProductList";

const ChatBotify = () => {
  async function callApi(params: Params) {
    const response = await fetch("http://localhost:3000/chat", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        "text": params.userInput,
      })
    });
    const data = await response.json();
    const responseMessage = data.answer.replace(/\n{2,}/g, '\n');
    params.injectMessage(marked(responseMessage));

    if (data.intent.intent === 'policy_faq') {
      params.injectMessage((
        <FaqAccordion faqs={data.faqs} />
      ));
    } else if (data.intent.intent === 'product_consultation') {
      params.injectMessage((
        <ProductList products={data.products} />
      ));
    }

  }

  const flow: Flow = {
    "start": {
      message: "Xin chào, tôi là một chatbot. Bạn cần giúp gì?",
      path: "loop"
    },
    "loop": {
      message: callApi,
      path: "loop",
    },
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
