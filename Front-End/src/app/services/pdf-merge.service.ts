import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class PdfMergeService {

  constructor(private http: HttpClient) { }

  public postMergeRequest(pdfFile: File[]) {
    const formData = this.createMergeForm(pdfFile);
    return this.http.post<Blob>("https://localhost:7242/api/pdf/merge", formData, {responseType: 'blob' as 'json'})
      .pipe(
        map(blob => {
            return blob
          }
        )
      );
  }

  private createMergeForm(pdfFiles : File[]) {
    const formData = new FormData();
    for(let i = 0; i < pdfFiles.length; i++){
      formData.append('pdfFiles', pdfFiles[i]);
    }

    return formData;
  }
}
