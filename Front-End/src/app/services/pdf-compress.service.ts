import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class PdfCompressService {

  constructor(private http: HttpClient) { }

  public postCompressRequest(pdfFile: File, compressLevel: number) {
    console.log(compressLevel)
    const formData = this.createCompressForm(pdfFile, compressLevel);
    console.log(formData.get("compressionLevel"))

    return this.http.post<Blob>("https://localhost:7242/api/pdf/watermark", formData, {responseType: 'blob' as 'json'})
      .pipe(
        map(blob => {
            return blob
          }
        )
      );
  }

  private createCompressForm(pdfFile: File, compressionLevel: number){
    const formData = new FormData();
    formData.append('pdfFile', pdfFile);
    formData.append('compressionLevel', compressionLevel.toString());
    return formData;
  }

}
