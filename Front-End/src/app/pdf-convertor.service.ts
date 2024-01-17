import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PdfConvertorService {

  constructor(private http: HttpClient) { }

  public convertToRequest(file: File, from: number, to: number){
    return new SplitRequest(file, from, to)
  }

  private convertFile(fileDTO: FileResponse){
    return fileDTO.pdfFile
  }

  public postSplitRequest(splitRequest: SplitRequest){
    return this.http.post<FileResponse>("api/pdf/split", splitRequest).pipe(map(this.convertFile))
  }

}

export class SplitRequest {
  pdfFile: File
  from: number
  to: number
  constructor(pdfFile: File, from: number, to: number) {
    this.pdfFile = pdfFile
    this.from = from
    this.to = to
  }
}

export interface FileResponse {
  pdfFile: File
}
