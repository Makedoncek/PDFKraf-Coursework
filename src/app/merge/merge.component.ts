import {Component, ElementRef, ViewChild} from '@angular/core';
import {PDFDocument} from "pdf-lib";

@Component({
  selector: 'app-merge',
  templateUrl: './merge.component.html',
  styleUrl: './merge.component.css'
})
export class MergeComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  selectedFiles: File[] = [];
  fileNames: String[] | undefined;

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
          const pdfDoc = await PDFDocument.load(pdfBytes);
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
}
