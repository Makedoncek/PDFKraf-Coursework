import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class PdfMergeService {

  constructor(private http: HttpClient) { }

  public postMergeRequest(pdfFile: File[]) {

    return this.http.post<Blob>("https://localhost:7242/api/pdf/merge", pdfFile, {responseType: 'blob' as 'json'})
      .pipe(
        map(blob => {
            return blob
          }
        )
      );
  }

}
