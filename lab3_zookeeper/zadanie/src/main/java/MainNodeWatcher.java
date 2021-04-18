import org.apache.zookeeper.*;
import java.io.IOException;

public class MainNodeWatcher implements Watcher {
    private final String exec;
    private final String znode;
    private final ZooKeeper zk;
    private Process process;

    public MainNodeWatcher(String exec, ZooKeeper zk, String znode) {
        this.exec = exec;
        this.zk = zk;
        this.znode = znode;
        checkIfExistAtStart();
    }

    @Override
    public void process(WatchedEvent watchedEvent) {
        if(watchedEvent.getType() == Event.EventType.NodeCreated){
            try {
                process = Runtime.getRuntime().exec(exec);
            } catch (IOException e) {
                e.printStackTrace();
            }
//            try {
//                zk.addWatch(znode, new ChildWatcher(zk, znode), AddWatchMode.PERSISTENT_RECURSIVE);
//            } catch (KeeperException | InterruptedException e) {
//                e.printStackTrace();
//            }
        }
        else if(watchedEvent.getType() == Event.EventType.NodeDeleted){
            process.destroy();
            System.out.println(znode + " deleted");
        }
        else {
            System.out.println(watchedEvent.getType());
            try {
                zk.addWatch(znode, new ChildWatcher(zk, znode), AddWatchMode.PERSISTENT_RECURSIVE);
            } catch (KeeperException | InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    private void checkIfExistAtStart() {
        try {
            if (zk.exists(znode, false) != null) {
                process = Runtime.getRuntime().exec(exec);
//                zk.addWatch(znode, new ChildWatcher(zk, znode), AddWatchMode.PERSISTENT_RECURSIVE);
            }
        } catch (InterruptedException | KeeperException | IOException e) {
            checkIfExistAtStart();
        }
    }
}
