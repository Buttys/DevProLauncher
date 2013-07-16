--ＣＸ 激烈華戦艦 タオヤメ
function c76419637.initial_effect(c)
	--xyz summon
	aux.AddXyzProcedure(c,aux.XyzFilterFunction(c,4),4)
	c:EnableReviveLimit()
	--pos&atk
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_DAMAGE)
	e1:SetProperty(EFFECT_FLAG_PLAYER_TARGET)
	e1:SetType(EFFECT_TYPE_IGNITION)
	e1:SetRange(LOCATION_MZONE)
	e1:SetCondition(c76419637.condition)
	e1:SetCost(c76419637.cost)
	e1:SetTarget(c76419637.target)
	e1:SetOperation(c76419637.operation)
	c:RegisterEffect(e1)
	local e2=Effect.CreateEffect(c)
	e2:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_TRIGGER_F)
	e2:SetCategory(CATEGORY_HANDES)
	e2:SetCode(EVENT_PHASE+PHASE_END)
	e2:SetRange(LOCATION_MZONE)
	e2:SetProperty(EFFECT_FLAG_REPEAT)
	e2:SetCountLimit(1)
	e2:SetCondition(c76419637.discon)
	e2:SetTarget(c76419637.distg)
	e2:SetOperation(c76419637.disop)
	c:RegisterEffect(e2)
end
function c76419637.condition(e)
	return e:GetHandler():GetOverlayGroup():IsExists(Card.IsCode,1,nil,40424929) 
end

function c76419637.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return e:GetHandler():CheckRemoveOverlayCard(tp,1,REASON_COST)
	and Duel.GetFlagEffect(tp,76419637)==0 end
	e:GetHandler():RemoveOverlayCard(tp,1,1,REASON_COST)
	Duel.RegisterFlagEffect(tp,76419637,RESET_PHASE+PHASE_DRAW,0,1)
end
function c76419637.target(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.IsExistingMatchingCard(aux.TRUE,tp,LOCATION_ONFIELD,LOCATION_ONFIELD,1,nil) end
	Duel.SetTargetPlayer(1-tp)
	local dam=Duel.GetFieldGroupCount(1-tp,LOCATION_ONFIELD,LOCATION_ONFIELD)*400
	Duel.SetTargetParam(dam)
	Duel.SetOperationInfo(0,CATEGORY_DAMAGE,nil,0,1-tp,dam)
end
function c76419637.operation(e,tp,eg,ep,ev,re,r,rp)
	local p=Duel.GetChainInfo(0,CHAININFO_TARGET_PLAYER)
	local dam=Duel.GetFieldGroupCount(1-tp,LOCATION_ONFIELD,LOCATION_ONFIELD)*400
	Duel.Damage(p,dam,REASON_EFFECT)
end
function c76419637.cost2(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFlagEffect(tp,76419637)==0 end
	Duel.RegisterFlagEffect(tp,76419637,RESET_PHASE+PHASE_DRAW,0,1)
end
function c76419637.discon(e,tp,eg,ep,ev,re,r,rp)
	return tp~=Duel.GetTurnPlayer() and Duel.GetFieldGroupCount(tp,LOCATION_HAND,0)<Duel.GetFieldGroupCount(tp,0,LOCATION_HAND)
end
function c76419637.distg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return true end
	Duel.SetOperationInfo(0,CATEGORY_HANDES,nil,0,1-tp,1)
end
function c76419637.disop(e,tp,eg,ep,ev,re,r,rp)
	Duel.DiscardHand(1-tp,nil,1,1,REASON_EFFECT,nil)
end