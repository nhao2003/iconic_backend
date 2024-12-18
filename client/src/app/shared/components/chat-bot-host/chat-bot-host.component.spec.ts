import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatBotHostComponent } from './chat-bot-host.component';

describe('ChatBotHostComponent', () => {
  let component: ChatBotHostComponent;
  let fixture: ComponentFixture<ChatBotHostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatBotHostComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatBotHostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
