import {NodeDto} from "./NodeDto.ts";
import {useEffect, useState} from "react";
import {Button, Icon, Segment} from "semantic-ui-react";
import agent from "../agent/agent.ts";

interface Props {
    node: NodeDto,
    isOpenedAll: boolean,
    refreshNodes: (isReversed: boolean) => void,
    isReversed: boolean
}
interface CreateNodeDto {
    name: string;
    parentNodeId: string
}

const Node = ({ node, isOpenedAll, refreshNodes, isReversed } : Props) => {

    const [isOpened, setIsOpened] = useState(false);
    const [clicked, setClicked] = useState(false);
    const [points, setPoints] = useState({ x: 0, y: 0 });
    const [addingChild, setAddingChild] = useState(false);
    const [newChildName, setNewChildName] = useState('');
    const [editing, setEditing] = useState(false);
    const [editedName, setEditedName] = useState(node.name);

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
            refreshNodes(isReversed);
        } catch (error) {
            console.error('Error adding node:', error);
        }
    };
    
    const handleEditNode = async () => {
        setEditing(true);
    }

    const handleSaveEditedNode = async () => {
        try {
            await agent.Nodes.edit({newName: editedName, id: node.id});
            setEditing(false);
            refreshNodes(isReversed);
        } catch (error) {
            console.error('Error updating node:', error);
        }
    };
    
    const handleDeleteNode = async () => {
        try {
            await agent.Nodes.delete(node.name);
            refreshNodes(isReversed);
        } catch (error) {
            console.error('Error deleting node:', error);
        }
    }

    useEffect(() => {
        const handleKeyPress = (e: KeyboardEvent) => {
            if (e.key === 'Enter' && addingChild && newChildName) {
                handleAddChildNode();
            }
            
            if(e.key === 'Enter' && editing && editedName) {
                handleSaveEditedNode();
            }
        };
        
        window.addEventListener('keypress', handleKeyPress);
        return () => {
            window.removeEventListener('keypress', handleKeyPress);
        };
    }, [addingChild, newChildName, editedName, editing]);

    return (
        <>
            <Button
                icon
                onClick={() => setIsOpened(!isOpened)}
                onContextMenu={handleContextMenu}
            >
                {isOpened || isOpenedAll ? <Icon name="folder open" /> : <Icon name="folder" />}
                {editing ? (
                    <input
                        type="text"
                        value={editedName}
                        onChange={(e) => setEditedName(e.target.value)}
                        onBlur={handleSaveEditedNode}
                    />
                ) : (
                    <span>{node.name}</span>
                )}
            </Button>
            {clicked && (
                <Segment top={points.y} left={points.x}>
                    <Button icon onClick={() => setAddingChild(true)}><Icon name="plus" /></Button>
                    <Button icon onClick={handleEditNode}><Icon name="edit" /></Button>
                    <Button icon onClick={handleDeleteNode}><Icon name="trash" /></Button>
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
                </div>
            )}
            {node.childrens && (isOpened || isOpenedAll) && (
                node.childrens.map((subnode) => (
                    <div key={subnode.id} style={{ paddingLeft: "20px" }}>
                        <Node node={subnode} isOpenedAll={isOpenedAll} isReversed={isReversed} refreshNodes={refreshNodes} />
                    </div>
                ))
            )}
        </>
    );
};

export default Node;

