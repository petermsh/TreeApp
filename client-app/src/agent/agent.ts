import axios from "axios";
import {NodeDto} from "../nodes/NodeDto.ts";

const Nodes = {
    create: async (newNode: CreateNodeDto) => axios.post<CreateNodeDto>('http://localhost:5095/Node', newNode)
        .catch((error) => {
            console.error('Error adding node:', error);
        }),
    
    list: async () => axios.get<NodeDto[]>('http://localhost:5095/Node')
        .catch((error) => {
            console.error('Error fetching tree data:', error);
        }),
}


interface CreateNodeDto {
    name: string;
    parentNodeId: string
}

const agent = {
    Nodes
}

export default agent;