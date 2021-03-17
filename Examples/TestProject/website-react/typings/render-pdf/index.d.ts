import * as React from 'react';

export type PdfSource = string | ArrayBuffer | Blob;

export interface Pdf {
    numPages: number;
}

export interface DocumentProps{
    file: PdfSource;
    children?: React.ReactNode;
    className?: string;
    loading?: string | React.ReactNode | Function;
    noData?: string | React.ReactNode | Function;
    onLoadError?: (error: any) => void; 
    onLoadSuccess?: (pdf: Pdf) => void;
    onSourceError?: (error: any) => void;
    onSourceSuccess?: () => void;
}

export class Document extends React.Component<DocumentProps> {}

export interface PageProps {
    className?: string;
    pageIndex?: number;
    scale?: number;
    width?: number;
    renderMode?: "canvas" | "svg";
    renderAnnotations?: boolean;
    renderTextLayer?: boolean;
}

export class Page extends React.Component<PageProps> {}

export function setOptions(options: {
    workerSrc?: string
}) : void;