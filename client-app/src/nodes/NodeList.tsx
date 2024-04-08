import Node from "./Node.tsx";
import {NodeDto} from "./NodeDto.ts";
import {Button, Icon, Segment} from "semantic-ui-react";
import {useEffect, useState} from "react";

interface Props {
    nodes: NodeDto[]
    refreshNodes: (isReversed: boolean) => void;
}

export default function NodeList({nodes, refreshNodes} : Props) {

    const [isOpenedAll, setIsOpenedAll] = useState(false);
    const [isReversed, setIsReversed] = useState(false);

    useEffect(() => {
        refreshNodes(isReversed);
        console.log('isReversed changed:', isReversed);
    }, [isReversed]);
    
    return(
        <Segment>
            <h1>Widok drzewa</h1>
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
                <Button icon  onClick={() => {setIsReversed(!isReversed)}}>
                    {isReversed ?
                        <>
                            <Icon name={"sort alphabet descending"} />
                            Zmień kolejność sortowania
                        </>
                        : <>
                            <Icon name={"sort alphabet ascending"} />
                            Zmień kolejność sortowania
                        </>
                    }
                </Button>
            </div>
            {nodes.map((n) => (
                <Node key={n.id} node={n} isOpenedAll={isOpenedAll} isReversed={isReversed} refreshNodes={refreshNodes} />
            ))}
        </Segment>
    )
}