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

  triggerInput() {
    this.fileInput.nativeElement.click();
  }

  onFileSelection(event: any) {
    this.selectedFiles?.push(...event.target.files);
    console.log(this.selectedFiles)

    this.getNumPages()
    this.getPDFName()
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
          this.fileNames?.push("Wrong ABOBAS please help me, I am drowning under the wotar")
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
    link.download = "compressed.pdf";
    link.click();
  }
}
