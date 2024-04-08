import {useEffect, useState} from "react";
import NodeList from "./nodes/NodeList.tsx";
import { NodeDto } from "./nodes/NodeDto";
import agent from "./agent/agent.ts";

function App() {

    const [treeData, setTreeData] = useState<NodeDto[]>([]);
    const [isReversedLocal, setIsReversedLocal] = useState(false);
  
    useEffect(() => {
        fetchData(isReversedLocal);
    }, [isReversedLocal]);

    const fetchData = async (isReversed: boolean) => {
        try {
            const response = await agent.Nodes.list(isReversed);
            if (response && response.data)
                setTreeData(response.data);
        } catch (error) {
            console.error('Error fetching tree data:', error);
        }
    };

    const refreshNodes = (isReversed: boolean) => {
        console.log(isReversed);
        console.log(isReversedLocal);
        if(isReversedLocal == isReversed)
            console.log("fetch");
            fetchData(isReversedLocal);
        setIsReversedLocal(isReversed);
    };
  
    
  
  console.log(treeData);
  
  return(
      <>
        <NodeList nodes={treeData} refreshNodes={refreshNodes} />
      </>
      
  );
}

export default App


