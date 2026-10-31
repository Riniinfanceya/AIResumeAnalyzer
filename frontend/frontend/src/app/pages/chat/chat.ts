import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ChatService } from '../../services/chat';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css'
})
export class Chat {

  question: string = '';

  answer: string = '';

  mode: string = '';

  constructor(
    private chatService: ChatService
  ) {}

  send() {

    if (!this.question.trim()) {
      return;
    }

    this.chatService
      .ask(this.question)
      .subscribe({
        next: (res) => {
          this.answer = res.answer;
          this.mode = res.mode;
        },
        error: (err) => {
          console.error(err);
        }
      });
  }
}