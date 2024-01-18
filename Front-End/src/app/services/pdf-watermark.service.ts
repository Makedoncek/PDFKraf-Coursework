import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class PdfWatermarkService {

  constructor(private http: HttpClient) { }

  public postWatermarkRequest(pdfFile: File, watermarkText: string) {
    const formData = this.createWatermarkForm(pdfFile, watermarkText);

    return this.http.post<Blob>("https://localhost:7242/api/pdf/watermark", formData, {responseType: 'blob' as 'json'})
      .pipe(
        map(blob => {
            return this.formFile(blob)
          }
        )
      );
  }

  private createWatermarkForm(pdfFile: File, watermarkText: string){
    const formData = new FormData();
    formData.append('pdfFile', pdfFile);
    formData.append('watermarkText', watermarkText);
    return formData;
  }

  private formFile(blob: Blob) {
    return new File([blob], "watermark.pdf", {type: 'application/pdf'})
  }
}
