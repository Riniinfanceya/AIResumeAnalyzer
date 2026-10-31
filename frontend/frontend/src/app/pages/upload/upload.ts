import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DocumentService } from '../../services/document';

@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './upload.html',
  styleUrl: './upload.css'
})
export class Upload {

  fileName = '';
  content = '';
  summary = '';

  message = '';

  constructor(
    private documentService: DocumentService
  ) {
  }

  save() {

    const document = {
      fileName: this.fileName,
      content: this.content,
      summary: this.summary
    };

    this.documentService
      .create(document)
      .subscribe({
        next: () => {

          this.message =
            'Document Saved Successfully';

          this.fileName = '';
          this.content = '';
          this.summary = '';
        },
        error: (err) => {
          console.log(err);
        }
      });
  }
}