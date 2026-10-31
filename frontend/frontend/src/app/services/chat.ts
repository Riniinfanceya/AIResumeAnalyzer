import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private apiUrl = 'https://localhost:7298/api/chat';

  constructor(private http: HttpClient) { }

  ask(question: string) {
    return this.http.post<any>(
      this.apiUrl,
      {
        question: question
      }
    );
  }
}