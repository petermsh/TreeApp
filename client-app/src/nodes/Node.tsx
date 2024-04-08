import {NodeDto} from "./NodeDto.ts";
import {useEffect, useState} from "react";
import {Button, Icon, Segment} from "semantic-ui-react";
import agent from "../agent/agent.ts";

interface Props {
    node: NodeDto,
    isOpenedAll: boolean,
    refreshNodes: () => void
}
interface CreateNodeDto {
    name: string;
    parentNodeId: string
}

const Node = ({ node, isOpenedAll, refreshNodes } : Props) => {

    const [isOpened, setIsOpened] = useState(false);
    const [clicked, setClicked] = useState(false);
    const [points, setPoints] = useState({ x: 0, y: 0 });
    const [addingChild, setAddingChild] = useState(false);
    const [newChildName, setNewChildName] = useState('');

    useEffect(() => {
        const handleClick = () => setClicked(false);
        window.addEventListener("click", handleClick);
        return () => {
            window.removeEventListener("click", handleClick);
        };
    }, []);

    const handleContextMenu = (e: React.MouseEvent) => {
        e.preventDefault();
        setClicked(true);
        setPoints({
            x: e.pageX,
            y: e.pageY,
        });
    };

    const handleAddChildNode = async () => {
        try {
            const newNode: CreateNodeDto = { name: newChildName, parentNodeId: node.id };
            await agent.Nodes.create(newNode);
            setAddingChild(false);
            setNewChildName(''); 
            refreshNodes();
        } catch (error) {
            console.error('Error adding node:', error);
        }
    };

    useEffect(() => {
        const handleKeyPress = (e: KeyboardEvent) => {
            if (e.key === 'Enter' && addingChild && newChildName) {
                handleAddChildNode();
            }
        };

        window.addEventListener('keypress', handleKeyPress);
        return () => {
            window.removeEventListener('keypress', handleKeyPress);
        };
    }, [addingChild, newChildName]);

    return (
        <>
            <Button
                icon
                onClick={() => setIsOpened(!isOpened)}
                onContextMenu={handleContextMenu}
            >
                {isOpened || isOpenedAll ? <Icon name="folder open" /> : <Icon name="folder" />}
                {node.name}
            </Button>
            {clicked && (
                <Segment top={points.y} left={points.x}>
                    <Button icon onClick={() => setAddingChild(true)}><Icon name="plus" /></Button>
                </Segment>
            )}
            {addingChild && (
                <div style={{ paddingLeft: "20px" }}>
                    <input
                        type="text"
                        value={newChildName}
                        onChange={(e) => setNewChildName(e.target.value)}
                        placeholder="Nazwa nowego węzła"
                    />
                    <Button icon onClick={handleAddChildNode}><Icon name="add" /></Button>
                </div>
            )}
            {node.childrens && (isOpened || isOpenedAll) && (
                node.childrens.map((subnode) => (
                    <div key={subnode.id} style={{ paddingLeft: "20px" }}>
                        <Node node={subnode} isOpenedAll={isOpenedAll} refreshNodes={refreshNodes} />
                    </div>
                ))
            )}
        </>
    );
};

export default Node;

