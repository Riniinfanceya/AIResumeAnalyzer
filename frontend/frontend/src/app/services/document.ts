import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private apiUrl =
    'https://localhost:7298/api/document';

  constructor(private http: HttpClient) {
  }

  create(document: any) {

    return this.http.post(
      this.apiUrl,
      document
    );
  }
}