import { Component } from '@angular/core';
import { Upload } from './pages/upload/upload';
import { Chat } from './pages/chat/chat';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Upload, Chat],
  template: `
    <h1 style="text-align:center">
      AI Resume Assistant
    </h1>

    <app-upload></app-upload>

    <hr>

    <app-chat></app-chat>
  `
})
export class App {
}