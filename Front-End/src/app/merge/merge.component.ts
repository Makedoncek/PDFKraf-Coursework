import {Component, ElementRef, ViewChild} from '@angular/core';
import {PDFDocument} from "pdf-lib";
import {PdfMergeService} from "../services/pdf-merge.service";

@Component({
  selector: 'app-merge',
  templateUrl: './merge.component.html',
  styleUrl: './merge.component.css'
})

export class MergeComponent {

  constructor(private service: PdfMergeService) {
  }

  @ViewChild('fileInput') fileInput!: ElementRef;
  selectedFiles: File[] = [];
  fileNames: String[] | undefined;
  result: Blob | undefined;
  fileSize: number = 0;

  triggerInput() {
    this.wipeData()
    this.fileInput.nativeElement.click();
  }

  onFileSelection(event: any) {
    this.selectedFiles?.push(...event.target.files);

    this.getNumPages()
    this.getPDFName()
    this.getFilesSize();
  }

  private getFilesSize() {
      this.fileSize += this.selectedFiles[this.selectedFiles.length - 1].size;
  }

  private getNumPages() {
    if (this.selectedFiles) {
      const reader = new FileReader();

      reader.onload = async (e: any) => {
        const pdfBytes = e.target.result;

        try {
          await PDFDocument.load(pdfBytes);
        }
        catch (error) {
          this.fileNames?.push("ERROR")
          this.selectedFiles = [];
          this.fileNames = undefined;
          this.fileSize = 0;

        }
      };
      reader.readAsArrayBuffer(this.selectedFiles[0]);
    }
  }

  getPDFName() {
    this.fileNames = this.selectedFiles?.map(it => it.name)
    console.log(this.fileNames)
  }

  sendMergeRequest(){
      this.service.postMergeRequest(this.selectedFiles).subscribe(blob => {this.result = blob})
  }

  downloadFile() {
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(this.result!);
    link.download = "merge_" + this.fileNames![0];
    link.click();
    this.wipeData()
  }

  private wipeData() {
    this.result = undefined
    
  }

  decideDisability(){
    return this.fileSize >= 27 * 1024 * 1024;
  }

}
