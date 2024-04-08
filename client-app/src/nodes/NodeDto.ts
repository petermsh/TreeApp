export interface NodeDto {
    id: string;
    name: string;
    parentNodeId: string;
    childrens: NodeDto[]
}