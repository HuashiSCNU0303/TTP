package edu.whu.xamarin.TTP.controller;

import com.google.gson.Gson;
import edu.whu.xamarin.TTP.model.Message;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;

import javax.websocket.*;
import javax.websocket.server.PathParam;
import javax.websocket.server.ServerEndpoint;
import java.io.IOException;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/**
 * @author Gjing
 **/
@ServerEndpoint("/test-one/{userId}")
@Component
@Slf4j
public class MyOneToOneServer {
    /**
     * 用于存放所有在线客户端
     */
    private static Map<Long, MyOneToOneServer> clients = new ConcurrentHashMap<>();

    private Gson gson = new Gson();

    private Session session;
    /**接收userId*/
    private Long userId;


    @OnOpen
    public void onOpen(Session session,@PathParam("userId") Long userId) {
        log.info("有新的客户端上线: {}", userId);
        this.session = session;
        this.userId=userId;
        clients.put(userId, this);
    }

    @OnClose
    public void onClose() {

        log.info("有客户端离线: {}", userId);
        clients.remove(userId);
    }

    @OnError
    public void onError(Session session, Throwable throwable) {
        if (clients.get(userId) != null) {
            log.info("发生了错误,移除客户端: {}", userId);
            clients.remove(userId);
        }
        throwable.printStackTrace();
    }

    @OnMessage
    public void onMessage(String message) {
        log.info("收到客户端发来的消息: {}", message);
        this.sendTo(gson.fromJson(message, Message.class));
    }

    /**
     * 发送消息
     *
     * @param message 消息对象
     */
    private void sendTo(Message message) {
        Session s;
        try{
            s = clients.get(message.getReceiverId()).session;
        }catch (Exception e){
            return;
        }
        if (s != null) {
            try {
                s.getBasicRemote().sendText(message.getMessage()+"|"+message.getSenderId().toString());
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
