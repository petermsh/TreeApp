import './App.css'
import {useEffect, useState} from "react";
import NodeList from "./nodes/NodeList.tsx";
import { NodeDto } from "./nodes/NodeDto";
import agent from "./agent/agent.ts";

function App() {

  const [treeData, setTreeData] = useState<NodeDto[]>([]);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            const response = await agent.Nodes.list();
            if (response && response.data)
                setTreeData(response.data);
        } catch (error) {
            console.error('Error fetching tree data:', error);
        }
    };

    const refreshNodes = () => {
        fetchData(); 
    };
  
    
  
  console.log(treeData);
  
  return(
      <>
        <NodeList nodes={treeData} refreshNodes={refreshNodes} />
      </>
      
  );
}

export default App


