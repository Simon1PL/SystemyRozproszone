import org.apache.zookeeper.*;

import java.io.IOException;
import java.util.Scanner;

public class Main {
    private static ZooKeeper zk;

    public static void main(String[] args) throws IOException, KeeperException, InterruptedException {
        String exec = "notepad.exe";
        String znode = "/z";
        String hosts = "localhost:2181,localhost:2182,localhost:2183";
        zk = new ZooKeeper(hosts,5000,null);
        zk.addWatch(znode, new MainNodeWatcher(exec, zk, znode), AddWatchMode.PERSISTENT);

        Scanner reader = new Scanner(System.in);
        while (true) {
            reader.nextLine();
            showChildrenTree(znode, "");
        }
    }

    private static void showChildrenTree(String node, String deep) {
        try {
            if (zk.exists(node, false) != null) {
                System.out.println(deep + node);
                for (String child : zk.getChildren(node, false)) {
                    showChildrenTree(node + "/" + child, deep + "\t");
                }
            }
            else {
                throw new KeeperException(KeeperException.Code.NONODE) {
                };
            }
        } catch (KeeperException e) {
            if (e.code() == KeeperException.Code.NONODE) {
                System.out.println("There is no node " + node);
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
