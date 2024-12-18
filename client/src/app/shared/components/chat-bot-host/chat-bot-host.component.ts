import { Component, ElementRef, AfterViewInit, OnDestroy } from '@angular/core';
import * as ReactDOM from 'react-dom/client'; // Sử dụng từ react-dom/client
import React from 'react';
import ChatBotify from '../../../../react-components/chat_botify';

@Component({
  selector: 'app-chat-bot-host',
  template: '<div id="react-root"></div>',
  standalone: true,
  styleUrls: ['./chat-bot-host.component.scss'],
})
export class ChatBotHostComponent implements AfterViewInit, OnDestroy {
  private reactRoot: ReactDOM.Root | null = null;

  constructor(private hostElement: ElementRef) {}

  ngAfterViewInit(): void {
    const rootElement = this.hostElement.nativeElement.querySelector('#react-root');
    this.reactRoot = ReactDOM.createRoot(rootElement); // Tạo "root" mới
    this.reactRoot.render(React.createElement(ChatBotify)); // Render React component
  }

  ngOnDestroy(): void {
    if (this.reactRoot) {
      this.reactRoot.unmount(); // Hủy bỏ React component khi Angular component bị destroy
    }
  }
}
