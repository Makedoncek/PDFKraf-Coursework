import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PdfConvertorService {

  constructor(private http: HttpClient) { }

  public convertToRequest(file: File, from: number, to: number){
    return new SplitPdfDTO(file, from, to)
  }



  public postSplitRequest(splitRequest: SplitPdfDTO) {
    const formData = new FormData();
    formData.append('pdfFile', splitRequest.pdfFile);
    formData.append('startPage', splitRequest.startPage.toString());
    formData.append('endPage', splitRequest.endPage.toString());

    return this.http.post<Blob>("https://localhost:7242/api/pdf/split", formData, { responseType: 'blob' as 'json' })
      .pipe(map(blob => {
        return new File([blob], "split.pdf", { type: 'application/pdf' })
      }));
  }



}

export class SplitPdfDTO {
  pdfFile: File
  startPage: number
  endPage: number
  constructor(pdfFile: File, from: number, to: number) {
    this.pdfFile = pdfFile
    this.startPage = from
    this.endPage = to
  }
}





export interface FileResponse {
  pdfFile: File
}
