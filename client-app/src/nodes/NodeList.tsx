import Node from "./Node.tsx";
import {NodeDto} from "./NodeDto.ts";
import {Button, Icon, Segment} from "semantic-ui-react";
import {useState} from "react";

interface Props {
    nodes: NodeDto[]
    refreshNodes: () => void;
}

export default function NodeList({nodes, refreshNodes} : Props) {

    const [isOpenedAll, setIsOpenedAll] = useState(false);
    
    
    
    return(
        <Segment>
            Widok drzewa
            <div>
                <Button icon  onClick={() => setIsOpenedAll(!isOpenedAll)}>
                    {isOpenedAll ? 
                        <>
                            <Icon name={"folder open"} />
                            Zwiń wszystkie
                        </>
                        : <>
                            <Icon name={"folder"} />
                            Rozwiń wszystkie
                        </> 
                        }
                </Button>
            </div>
            {nodes.map((n) => (
                <Node key={n.id} node={n} isOpenedAll={isOpenedAll} refreshNodes={refreshNodes} />
            ))}
        </Segment>
    )
}