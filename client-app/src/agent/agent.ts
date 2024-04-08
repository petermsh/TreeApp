import axios from "axios";
import {NodeDto} from "../nodes/NodeDto.ts";

const Nodes = {
    create: async (newNode: CreateNodeDto) => axios.post<CreateNodeDto>('http://localhost:5095/Node', newNode)
        .catch((error) => {
            console.error('Error adding node:', error);
        }),

    edit: async (editedNode: EditNodeDto) => axios.put<CreateNodeDto>('http://localhost:5095/Node', editedNode)
        .catch((error) => {
            console.error('Error adding node:', error);
        }),

    delete: async (name: string) => axios.delete<CreateNodeDto>('http://localhost:5095/Node', {params: {name}})
        .catch((error) => {
            console.error('Error adding node:', error);
        }),
    
    list: async (isReversed: boolean) => axios.get<NodeDto[]>('http://localhost:5095/Node', {params: { isReversed }})
        .catch((error) => {
            console.error('Error fetching tree data:', error);
        }),
}


interface CreateNodeDto {
    name: string;
    parentNodeId: string
}

interface EditNodeDto {
    parentNodeId?: string;
    newName?: string;
    id: string
}

const agent = {
    Nodes
}

export default agent;